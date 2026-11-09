defmodule Jitsu.TaskLog do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          level: String.t(),
          logger: String.t(),
          message: String.t(),
          sync_id: String.t(),
          task_id: String.t(),
          timestamp: DateTime.t()
        }

  schema "task_log" do
    field(:level, :string)
    field(:logger, :string)
    field(:message, :string)
    field(:sync_id, :string)
    field(:task_id, :string)
    field(:timestamp, Jitsu.PrismaDateTime, autogenerate: true)
  end
end