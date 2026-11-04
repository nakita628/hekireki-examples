defmodule Langfuse.Dashboard do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          description: String.t(),
          definition: map(),
          filters: map(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          created_by_user: Langfuse.User.t() | nil,
          updated_by_user: Langfuse.User.t() | nil,
          project: Langfuse.Project.t() | nil,
          home_dashboard_projects: [Langfuse.Project.t()]
        }

  schema "dashboards" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:description, :string)
    field(:definition, :map)
    field(:filters, :map)
    belongs_to(:created_by_user, Langfuse.User, foreign_key: :created_by, type: :string)
    belongs_to(:updated_by_user, Langfuse.User, foreign_key: :updated_by, type: :string)
    belongs_to(:project, Langfuse.Project, foreign_key: :project_id, type: :string)
    has_many(:home_dashboard_projects, Langfuse.Project, foreign_key: :home_dashboard_id, references: :id)
    timestamps(inserted_at_source: :created_at)
  end
end