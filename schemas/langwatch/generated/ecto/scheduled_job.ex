defmodule LangWatch.ScheduledJob do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          project_id: String.t(),
          target_type: String.t(),
          target_id: String.t(),
          cron: String.t(),
          timezone: String.t(),
          next_run_at: DateTime.t(),
          last_slot: DateTime.t() | nil,
          current_slot: DateTime.t() | nil,
          attempts: integer(),
          last_error: String.t() | nil,
          active: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "ScheduledJob" do
    field(:id, :string, primary_key: true)
    field(:project_id, :string, source: :projectId)
    field(:target_type, :string, source: :targetType)
    field(:target_id, :string, source: :targetId)
    field(:cron, :string)
    field(:timezone, :string)
    field(:next_run_at, LangWatch.PrismaDateTime, source: :nextRunAt)
    field(:last_slot, LangWatch.PrismaDateTime, source: :lastSlot)
    field(:current_slot, LangWatch.PrismaDateTime, source: :currentSlot)
    field(:attempts, :integer, default: 0)
    field(:last_error, :string, source: :lastError)
    field(:active, :boolean, default: true)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end