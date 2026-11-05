defmodule OpenPanel.ShareReport do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: OpenPanel.PrismaDateTime, autogenerate: {OpenPanel.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          public: boolean(),
          password: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          report: OpenPanel.Report.t() | nil,
          organization: OpenPanel.Organization.t() | nil,
          project: OpenPanel.Project.t() | nil
        }

  schema "share_reports" do
    field(:id, :string, primary_key: true)
    field(:public, :boolean, default: false)
    field(:password, :string)
    field(:report_id, :string, source: :reportId)
    field(:organization_id, :string, source: :organizationId)
    field(:project_id, :string, source: :projectId)
    belongs_to(:report, OpenPanel.Report, foreign_key: :report_id, define_field: false, type: :string)
    belongs_to(:organization, OpenPanel.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    belongs_to(:project, OpenPanel.Project, foreign_key: :project_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end