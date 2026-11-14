defmodule LangWatch.NurturingOrganization do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          organization_id: String.t(),
          admin_user_id: String.t() | nil,
          seeded: boolean(),
          evaluation_count: integer(),
          simulation_run_count: integer(),
          coding_first_session_at: DateTime.t() | nil,
          coding_agent_types: [String.t()],
          coding_active_days: integer(),
          coding_last_active_day: String.t() | nil,
          coding_terminal_types: [String.t()],
          coding_repository_keys: [String.t()],
          coding_first_days: [integer()],
          last_trace_at: DateTime.t() | nil,
          updated_at: DateTime.t()
        }

  schema "NurturingOrganization" do
    field(:organization_id, :string, primary_key: true, source: :organizationId)
    field(:admin_user_id, :string, source: :adminUserId)
    field(:seeded, :boolean, default: false)
    field(:evaluation_count, :integer, default: 0, source: :evaluationCount)
    field(:simulation_run_count, :integer, default: 0, source: :simulationRunCount)
    field(:coding_first_session_at, LangWatch.PrismaDateTime, source: :codingFirstSessionAt)
    field(:coding_agent_types, {:array, :string}, source: :codingAgentTypes)
    field(:coding_active_days, :integer, default: 0, source: :codingActiveDays)
    field(:coding_last_active_day, :string, source: :codingLastActiveDay)
    field(:coding_terminal_types, {:array, :string}, source: :codingTerminalTypes)
    field(:coding_repository_keys, {:array, :string}, source: :codingRepositoryKeys)
    field(:coding_first_days, {:array, :integer}, source: :codingFirstDays)
    field(:last_trace_at, LangWatch.PrismaDateTime, source: :lastTraceAt)
    timestamps(inserted_at: false, updated_at_source: :updatedAt)
  end
end