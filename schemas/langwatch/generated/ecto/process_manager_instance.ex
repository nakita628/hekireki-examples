defmodule LangWatch.ProcessManagerInstance do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          process_name: String.t(),
          project_id: String.t(),
          process_key: String.t(),
          tenant_id: String.t(),
          user_id: String.t() | nil,
          state: map(),
          revision: integer(),
          next_wake_at: DateTime.t() | nil,
          updated_at: DateTime.t()
        }

  schema "ProcessManagerInstance" do
    field(:id, :string, primary_key: true)
    field(:process_name, :string, source: :processName)
    field(:project_id, :string, source: :projectId)
    field(:process_key, :string, source: :processKey)
    field(:tenant_id, :string, source: :tenantId)
    field(:user_id, :string, source: :userId)
    field(:state, :map)
    field(:revision, :integer)
    field(:next_wake_at, LangWatch.PrismaDateTime, source: :nextWakeAt)
    field(:updated_at, LangWatch.PrismaDateTime, source: :updatedAt)
  end
end