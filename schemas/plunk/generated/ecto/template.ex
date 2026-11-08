defmodule Plunk.Template do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Plunk.PrismaDateTime, autogenerate: {Plunk.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          name: String.t(),
          description: String.t() | nil,
          subject: String.t(),
          body: String.t(),
          from: String.t(),
          from_name: String.t() | nil,
          reply_to: String.t() | nil,
          type: atom(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Plunk.Project.t() | nil,
          emails: [Plunk.Email.t()],
          workflow_steps: [Plunk.WorkflowStep.t()]
        }

  schema "templates" do
    field(:name, :string)
    field(:description, :string)
    field(:subject, :string)
    field(:body, :string)
    field(:from, :string)
    field(:from_name, :string, source: :fromName)
    field(:reply_to, :string, source: :replyTo)
    field(:type, Ecto.Enum, values: [:TRANSACTIONAL, :MARKETING, :HEADLESS], default: :MARKETING)
    field(:project_id, :binary_id, source: :projectId)
    belongs_to(:project, Plunk.Project, foreign_key: :project_id, define_field: false)
    has_many(:emails, Plunk.Email, foreign_key: :template_id)
    has_many(:workflow_steps, Plunk.WorkflowStep, foreign_key: :template_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end