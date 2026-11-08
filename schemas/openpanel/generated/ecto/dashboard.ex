defmodule OpenPanel.Dashboard do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: OpenPanel.PrismaDateTime, autogenerate: {OpenPanel.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          filters: map(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: OpenPanel.Organization.t() | nil,
          project: OpenPanel.Project.t() | nil,
          share: OpenPanel.ShareDashboard.t() | nil,
          reports: [OpenPanel.Report.t()]
        }

  schema "dashboards" do
    field(:id, :string, primary_key: true, read_after_writes: true)
    field(:name, :string)
    field(:filters, :map)
    field(:organization_id, :string, source: :organizationId)
    field(:project_id, :string, source: :projectId)
    belongs_to(:organization, OpenPanel.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    belongs_to(:project, OpenPanel.Project, foreign_key: :project_id, define_field: false, type: :string)
    has_one(:share, OpenPanel.ShareDashboard, foreign_key: :dashboard_id, references: :id)
    has_many(:reports, OpenPanel.Report, foreign_key: :dashboard_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end