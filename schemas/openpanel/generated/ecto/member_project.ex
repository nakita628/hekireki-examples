defmodule OpenPanel.MemberProject do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: OpenPanel.PrismaDateTime, autogenerate: {OpenPanel.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          member_id: String.t(),
          project_id: String.t(),
          level: atom() | nil,
          inserted_at: DateTime.t(),
          member: OpenPanel.Member.t() | nil,
          project: OpenPanel.Project.t() | nil
        }

  schema "member_projects" do
    field(:level, Ecto.Enum, values: [:read, :write, :admin])
    field(:member_id, :string, primary_key: true, source: :memberId)
    field(:project_id, :string, primary_key: true, source: :projectId)
    belongs_to(:member, OpenPanel.Member, foreign_key: :member_id, define_field: false, type: :string)
    belongs_to(:project, OpenPanel.Project, foreign_key: :project_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end