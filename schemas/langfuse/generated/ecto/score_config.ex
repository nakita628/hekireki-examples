defmodule Langfuse.ScoreConfig do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          data_type: atom(),
          is_archived: boolean(),
          min_value: float() | nil,
          max_value: float() | nil,
          categories: map() | nil,
          description: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Langfuse.Project.t() | nil
        }

  schema "score_configs" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:data_type, Ecto.Enum, values: [:CATEGORICAL, :NUMERIC, :BOOLEAN, :TEXT])
    field(:is_archived, :boolean, default: false)
    field(:min_value, :float)
    field(:max_value, :float)
    field(:categories, :map)
    field(:description, :string)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end