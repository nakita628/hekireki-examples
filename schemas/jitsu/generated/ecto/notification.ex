defmodule Jitsu.Notification do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          workspace_id: String.t(),
          actor_id: String.t(),
          type: String.t(),
          table_name: String.t(),
          channel_id: String.t(),
          status_change_id: integer(),
          timestamp: DateTime.t(),
          status: String.t(),
          error: String.t() | nil
        }

  schema "Notification" do
    field(:workspace_id, :string, source: :workspaceId)
    field(:actor_id, :string, source: :actorId)
    field(:type, :string)
    field(:table_name, :string, source: :tableName)
    field(:channel_id, :string, source: :channelId)
    field(:status_change_id, :integer, source: :statusChangeId)
    field(:timestamp, Jitsu.PrismaDateTime, autogenerate: true)
    field(:status, :string)
    field(:error, :string)
  end
end