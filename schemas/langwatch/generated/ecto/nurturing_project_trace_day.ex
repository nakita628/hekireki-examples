defmodule LangWatch.NurturingProjectTraceDay do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          project_id: String.t(),
          day: String.t(),
          trace_count: integer(),
          error_count: integer(),
          last_trace_at: DateTime.t() | nil,
          last_failed_trace_at: DateTime.t() | nil
        }

  schema "NurturingProjectTraceDay" do
    field(:project_id, :string, primary_key: true, source: :projectId)
    field(:day, :string, primary_key: true)
    field(:trace_count, :integer, default: 0, source: :traceCount)
    field(:error_count, :integer, default: 0, source: :errorCount)
    field(:last_trace_at, LangWatch.PrismaDateTime, source: :lastTraceAt)
    field(:last_failed_trace_at, LangWatch.PrismaDateTime, source: :lastFailedTraceAt)
  end
end