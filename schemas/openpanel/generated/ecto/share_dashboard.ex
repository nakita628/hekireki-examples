defmodule OpenPanel.ShareDashboard do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: OpenPanel.PrismaDateTime, autogenerate: {OpenPanel.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          public: boolean(),
          password: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          dashboard: OpenPanel.Dashboard.t() | nil,
          organization: OpenPanel.Organization.t() | nil,
          project: OpenPanel.Project.t() | nil
        }

  schema "share_dashboards" do
    field(:id, :string, primary_key: true)
    field(:public, :boolean, default: false)
    field(:password, :string)
    field(:dashboard_id, :string, source: :dashboardId)
    field(:organization_id, :string, source: :organizationId)
    field(:project_id, :string, source: :projectId)
    belongs_to(:dashboard, OpenPanel.Dashboard, foreign_key: :dashboard_id, define_field: false, type: :string)
    belongs_to(:organization, OpenPanel.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    belongs_to(:project, OpenPanel.Project, foreign_key: :project_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end