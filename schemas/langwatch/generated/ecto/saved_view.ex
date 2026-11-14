defmodule LangWatch.SavedView do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          filters: map(),
          query: String.t() | nil,
          period: map() | nil,
          order: integer(),
          kind: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: LangWatch.Project.t() | nil,
          user: LangWatch.User.t() | nil
        }

  schema "SavedView" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:filters, :map, default: %{})
    field(:query, :string)
    field(:period, :map)
    field(:order, :integer, default: 0)
    field(:kind, :string, default: "v1-traces-filter")
    field(:project_id, :string, source: :projectId)
    field(:user_id, :string, source: :userId)
    belongs_to(:project, LangWatch.Project, foreign_key: :project_id, define_field: false, type: :string)
    belongs_to(:user, LangWatch.User, foreign_key: :user_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end