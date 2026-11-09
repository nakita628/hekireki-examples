defmodule Jitsu.StatusChange do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}

  @type t :: %__MODULE__{
          id: integer(),
          workspace_id: String.t(),
          actor_id: String.t(),
          type: String.t(),
          table_name: String.t(),
          timestamp: DateTime.t(),
          status: String.t(),
          started_at: DateTime.t(),
          counts: integer(),
          description: String.t() | nil,
          queue_size: integer()
        }

  schema "StatusChange" do
    field(:workspace_id, :string, source: :workspaceId)
    field(:actor_id, :string, source: :actorId)
    field(:type, :string)
    field(:table_name, :string, source: :tableName)
    field(:timestamp, Jitsu.PrismaDateTime, autogenerate: true)
    field(:status, :string)
    field(:started_at, Jitsu.PrismaDateTime, source: :startedAt)
    field(:counts, :integer, default: 0)
    field(:description, :string)
    field(:queue_size, :integer, default: 0, source: :queueSize)
  end
end