defmodule Jitsu.SourceTask do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          sync_id: String.t(),
          task_id: String.t(),
          package: String.t(),
          version: String.t(),
          started_at: DateTime.t(),
          updated_at: DateTime.t(),
          started_by: map() | nil,
          status: String.t(),
          description: String.t() | nil,
          metrics: map() | nil,
          error: String.t() | nil
        }

  schema "source_task" do
    field(:sync_id, :string)
    field(:task_id, :string, primary_key: true)
    field(:package, :string)
    field(:version, :string)
    field(:started_at, Jitsu.PrismaDateTime, autogenerate: true)
    field(:updated_at, Jitsu.PrismaDateTime, autogenerate: true)
    field(:started_by, :map)
    field(:status, :string)
    field(:description, :string)
    field(:metrics, :map)
    field(:error, :string)
  end
end