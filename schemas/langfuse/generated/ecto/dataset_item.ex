defmodule Langfuse.DatasetItem do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          project_id: String.t(),
          status: atom() | nil,
          input: map() | nil,
          expected_output: map() | nil,
          metadata: map() | nil,
          source_trace_id: String.t() | nil,
          source_observation_id: String.t() | nil,
          dataset_id: String.t(),
          valid_from: DateTime.t(),
          valid_to: DateTime.t() | nil,
          is_deleted: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "dataset_items" do
    field(:id, :string, primary_key: true)
    field(:project_id, :string, primary_key: true)
    field(:status, Ecto.Enum, values: [:ACTIVE, :ARCHIVED], default: :ACTIVE)
    field(:input, :map)
    field(:expected_output, :map)
    field(:metadata, :map)
    field(:source_trace_id, :string)
    field(:source_observation_id, :string)
    field(:dataset_id, :string)
    field(:valid_from, Langfuse.PrismaDateTime, primary_key: true, autogenerate: true)
    field(:valid_to, Langfuse.PrismaDateTime)
    field(:is_deleted, :boolean, default: false)
    timestamps(inserted_at_source: :created_at)
  end
end