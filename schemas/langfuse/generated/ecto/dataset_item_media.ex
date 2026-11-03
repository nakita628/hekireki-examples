defmodule Langfuse.DatasetItemMedia do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          project_id: String.t(),
          media_id: String.t(),
          dataset_id: String.t(),
          dataset_item_id: String.t(),
          dataset_item_valid_from: DateTime.t() | nil,
          field: String.t(),
          json_path: String.t() | nil,
          reference_string: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "dataset_item_media" do
    field(:id, :string, primary_key: true)
    field(:project_id, :string)
    field(:media_id, :string)
    field(:dataset_id, :string)
    field(:dataset_item_id, :string)
    field(:dataset_item_valid_from, Langfuse.PrismaDateTime)
    field(:field, :string)
    field(:json_path, :string)
    field(:reference_string, :string)
    timestamps(inserted_at_source: :created_at)
  end
end