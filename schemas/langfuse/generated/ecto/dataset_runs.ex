defmodule Langfuse.DatasetRuns do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          project_id: String.t(),
          name: String.t(),
          description: String.t() | nil,
          metadata: map() | nil,
          dataset_id: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "dataset_runs" do
    field(:id, :string, primary_key: true)
    field(:project_id, :string, primary_key: true)
    field(:name, :string)
    field(:description, :string)
    field(:metadata, :map)
    field(:dataset_id, :string)
    timestamps(inserted_at_source: :created_at)
  end
end