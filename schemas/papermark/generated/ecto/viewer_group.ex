defmodule Papermark.ViewerGroup do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          domains: [String.t()],
          allow_all: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          dataroom: Papermark.Dataroom.t() | nil,
          team: Papermark.Team.t() | nil,
          members: [Papermark.ViewerGroupMembership.t()],
          links: [Papermark.Link.t()],
          access_controls: [Papermark.ViewerGroupAccessControls.t()],
          views: [Papermark.View.t()],
          conversations: [Papermark.Conversation.t()],
          invitations: [Papermark.ViewerInvitation.t()],
          question_assignments: [Papermark.DataroomQuestionAssignment.t()],
          task_assignments: [Papermark.TaskAssignment.t()]
        }

  schema "ViewerGroup" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:domains, {:array, :string})
    field(:allow_all, :boolean, default: false, source: :allowAll)
    field(:dataroom_id, :string, source: :dataroomId)
    field(:team_id, :string, source: :teamId)
    belongs_to(:dataroom, Papermark.Dataroom, foreign_key: :dataroom_id, define_field: false, type: :string)
    belongs_to(:team, Papermark.Team, foreign_key: :team_id, define_field: false, type: :string)
    has_many(:members, Papermark.ViewerGroupMembership, foreign_key: :group_id, references: :id)
    has_many(:links, Papermark.Link, foreign_key: :group_id, references: :id)
    has_many(:access_controls, Papermark.ViewerGroupAccessControls, foreign_key: :group_id, references: :id)
    has_many(:views, Papermark.View, foreign_key: :group_id, references: :id)
    has_many(:conversations, Papermark.Conversation, foreign_key: :viewer_group_id, references: :id)
    has_many(:invitations, Papermark.ViewerInvitation, foreign_key: :group_id, references: :id)
    has_many(:question_assignments, Papermark.DataroomQuestionAssignment, foreign_key: :group_id, references: :id)
    has_many(:task_assignments, Papermark.TaskAssignment, foreign_key: :group_id, references: :id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end