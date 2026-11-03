defmodule Papermark.Workflow do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Papermark.PrismaDateTime, autogenerate: {Papermark.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          description: String.t() | nil,
          is_active: boolean(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          entry_link: Papermark.Link.t() | nil,
          team: Papermark.Team.t() | nil,
          steps: [Papermark.WorkflowStep.t()],
          executions: [Papermark.WorkflowExecution.t()]
        }

  schema "Workflow" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:description, :string)
    field(:is_active, :boolean, default: true, source: :isActive)
    field(:entry_link_id, :string, source: :entryLinkId)
    field(:team_id, :string, source: :teamId)
    belongs_to(:entry_link, Papermark.Link, foreign_key: :entry_link_id, define_field: false, type: :string)
    belongs_to(:team, Papermark.Team, foreign_key: :team_id, define_field: false, type: :string)
    has_many(:steps, Papermark.WorkflowStep, foreign_key: :workflow_id)
    has_many(:executions, Papermark.WorkflowExecution, foreign_key: :workflow_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end