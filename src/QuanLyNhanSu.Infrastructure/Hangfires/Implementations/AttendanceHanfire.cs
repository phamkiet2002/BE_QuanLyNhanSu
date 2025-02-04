using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyNhanSu.Contract.Abstractions.Message;
using QuanLyNhanSu.Contract.Service.V1.Attendance;
using QuanLyNhanSu.Infrastructure.Hangfires.Abstractions;

namespace QuanLyNhanSu.Infrastructure.Hangfires.Implementations;
public sealed class AttendanceHanfire : IAttendanceHangfire
{
    private readonly ICommandHandler<Command.CheckIsAbsentAttendanceCommand> _checkAbsentCommandHandler;

    public AttendanceHanfire(ICommandHandler<Command.CheckIsAbsentAttendanceCommand> checkAbsentCommandHandler)
    {
        _checkAbsentCommandHandler = checkAbsentCommandHandler;
    }

    public async Task CheckAbsentAttendanceAsync()
    {
        await _checkAbsentCommandHandler.Handle(new Command.CheckIsAbsentAttendanceCommand(), CancellationToken.None);
    }
}
