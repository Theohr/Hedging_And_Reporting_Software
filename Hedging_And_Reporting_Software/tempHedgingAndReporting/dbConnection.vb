Imports System.Data
Imports Oracle.ManagedDataAccess.Client
Imports Oracle.ManagedDataAccess.Types
Imports System.Data.OracleClient

Module dbConnection

    Public oradb = "Data Source="";Persist Security Info=True;User ID="";Password="";"
    Public orclConn As New OracleConnection(oradb)

End Module
