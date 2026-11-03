defmodule Langfuse.BackgroundMigration do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          script: String.t(),
          args: map(),
          state: map(),
          finished_at: DateTime.t() | nil,
          failed_at: DateTime.t() | nil,
          failed_reason: String.t() | nil,
          worker_id: String.t() | nil,
          locked_at: DateTime.t() | nil
        }

  schema "background_migrations" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:script, :string)
    field(:args, :map)
    field(:state, :map, default: %{})
    field(:finished_at, Langfuse.PrismaDateTime)
    field(:failed_at, Langfuse.PrismaDateTime)
    field(:failed_reason, :string)
    field(:worker_id, :string)
    field(:locked_at, Langfuse.PrismaDateTime)
  end
end