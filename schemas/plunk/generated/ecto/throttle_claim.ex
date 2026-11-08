defmodule Plunk.ThrottleClaim do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Plunk.PrismaDateTime, autogenerate: {Plunk.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          key: String.t(),
          recipient_key: String.t(),
          email_id: String.t() | nil,
          window_seconds: integer() | nil,
          expires_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Plunk.Project.t() | nil,
          contact: Plunk.Contact.t() | nil,
          workflow: Plunk.Workflow.t() | nil
        }

  schema "throttle_claims" do
    field(:key, :string)
    field(:recipient_key, :string, source: :recipientKey)
    field(:email_id, :string, source: :emailId)
    field(:window_seconds, :integer, source: :windowSeconds)
    field(:expires_at, Plunk.PrismaDateTime, source: :expiresAt)
    field(:project_id, :binary_id, source: :projectId)
    field(:contact_id, :binary_id, source: :contactId)
    field(:workflow_id, :binary_id, source: :workflowId)
    belongs_to(:project, Plunk.Project, foreign_key: :project_id, define_field: false)
    belongs_to(:contact, Plunk.Contact, foreign_key: :contact_id, define_field: false)
    belongs_to(:workflow, Plunk.Workflow, foreign_key: :workflow_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end