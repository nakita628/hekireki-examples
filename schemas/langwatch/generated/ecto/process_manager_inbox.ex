defmodule LangWatch.ProcessManagerInbox do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          process_name: String.t(),
          project_id: String.t(),
          process_key: String.t(),
          tenant_id: String.t(),
          source_event_id: String.t(),
          source_event_key: String.t() | nil,
          consumed_at: DateTime.t()
        }

  schema "ProcessManagerInbox" do
    field(:id, :string, primary_key: true)
    field(:process_name, :string, source: :processName)
    field(:project_id, :string, source: :projectId)
    field(:process_key, :string, source: :processKey)
    field(:tenant_id, :string, source: :tenantId)
    field(:source_event_id, :string, source: :sourceEventId)
    field(:source_event_key, :string, source: :sourceEventKey)
    field(:consumed_at, LangWatch.PrismaDateTime, source: :consumedAt)
  end
end