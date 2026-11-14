defmodule LangWatch.Analytics do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          key: String.t(),
          value: map() | nil,
          numeric_value: float() | nil,
          string_value: String.t() | nil,
          bool_value: boolean() | nil,
          inserted_at: DateTime.t(),
          project: LangWatch.Project.t() | nil
        }

  schema "Analytics" do
    field(:id, :string, primary_key: true)
    field(:key, :string)
    field(:value, :map)
    field(:numeric_value, :float, source: :numericValue)
    field(:string_value, :string, source: :stringValue)
    field(:bool_value, :boolean, source: :boolValue)
    field(:project_id, :string, source: :projectId)
    belongs_to(:project, LangWatch.Project, foreign_key: :project_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end