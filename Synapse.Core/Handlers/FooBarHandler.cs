﻿using System;
using System.Collections.Generic;
using System.Linq;

using Synapse.Core;

public class FooHandler : EmptyHandler
{
    override public ExecuteResult Execute(HandlerStartInfo startInfo)
    {
        int seq = 1;
        StatusType st = StatusType.Failed;

        try
        {
            Dictionary<object, object> p = Synapse.Core.Utilities.YamlHelpers.Deserialize( startInfo.Parameters );
            st = (StatusType)Enum.Parse( typeof( StatusType ), p.Values.ElementAt( 0 ).ToString() );
        }
        catch { }

        string x = $"{startInfo.ParentExitData}";

        OnLogMessage( "FooExecute", $"   - [[id]:[{startInfo.InstanceId}]] -   ----------   {startInfo.ParentExitData}   ---------- working ----------" );

        System.Threading.Thread.Sleep( 3000 );
        //Int64 j = 0;
        //for( Int64 i = 0; i < 2000000000; i++ )
        //    j = i - 1;

        bool cancel = OnProgress( "FooExecute", getMsg( StatusType.Initializing, startInfo ), StatusType.Initializing, startInfo.InstanceId, seq++ );
        OnLogMessage( "FooExecute", $"   - [[id]:[{startInfo.InstanceId}]] -   ----------   loop complete   ---------- working ----------" );

        if( !cancel )
        {
            OnProgress( "FooExecute", getMsg( StatusType.Running, startInfo ), StatusType.Running, startInfo.InstanceId, seq++ );
            if( !startInfo.IsDryRun ) { OnProgress( "FooExecute", "...Progress...", StatusType.Running, startInfo.InstanceId, seq++ ); }
            //throw new Exception( "quitting" );
            OnProgress( "FooExecute", "Finished", st, startInfo.InstanceId, seq++ );
            OnLogMessage( "FooExecute", $"Finished   - [[id]:[{startInfo.InstanceId}]] -" );
        }
        else
        {
            st = StatusType.Cancelled;
            OnProgress( "FooExecute", "Cancelled", st, startInfo.InstanceId, seq++ );
            OnLogMessage( "FooExecute", $"Cancelled   - [[id]:[{startInfo.InstanceId}]] -" );
        }

        WriteFile( "FooHandler", $"parms:{startInfo.Parameters}\r\nstatus:{st}\r\n-->CurrentPrincipal:{System.Security.Principal.WindowsIdentity.GetCurrent().Name}" );
        return new ExecuteResult() { Status = st, ExitData = "foo" };
    }

    protected string getMsg(StatusType status, HandlerStartInfo si)
    {
        return $"{status}-->dryRun:{si.IsDryRun}, ReqNum:{si.RequestNumber}";
    }

    protected void WriteFile(string handler, string message)
    {
        //string user = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split( '\\' )[1];
        //string fn = $"{ActionName}_{user}_{handler}_{DateTime.Now.Ticks}_{Guid.NewGuid()}";
        //System.IO.File.AppendAllText( fn, message ); ;
    }
}

public class BarHandler : FooHandler
{
    override public ExecuteResult Execute(HandlerStartInfo startInfo)
    {
        int seq = 1;

        string x = $"{startInfo.ParentExitData}";

        System.Threading.Thread.Sleep( 5000 );
        StatusType st = StatusType.Complete;
        bool cancel = OnProgress( "BarExecute", getMsg( StatusType.Initializing, startInfo ), StatusType.Initializing, startInfo.InstanceId, seq++ );
        OnLogMessage( "BarExecute", $"   ----------   {startInfo.ParentExitData}   ---------- working ----------" );
        if( !cancel )
        {
            OnProgress( "BarExecute", getMsg( StatusType.Running, startInfo ), StatusType.Running, startInfo.InstanceId, seq++ );
            if( !startInfo.IsDryRun ) { OnProgress( "BarExecute", "...Progress...", StatusType.Running, startInfo.InstanceId, seq++ ); }
            OnProgress( "BarExecute", "Finished", st, startInfo.InstanceId, seq++ );
        }
        else
        {
            st = StatusType.Cancelled;
            OnProgress( "BarExecute", "Cancelled", st, startInfo.InstanceId, seq++ );
        }
        WriteFile( "BarHandler", $"parms:{startInfo.Parameters}\r\nstatus:{st}\r\n-->CurrentPrincipal:{System.Security.Principal.WindowsIdentity.GetCurrent().Name}" );
        return new ExecuteResult() { Status = st, ExitData = "bar" };
    }
}