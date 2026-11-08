defmodule Plunk.Contact do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Plunk.PrismaDateTime, autogenerate: {Plunk.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          email: String.t(),
          data: map() | nil,
          subscribed: boolean(),
          snoozed_until: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          project: Plunk.Project.t() | nil,
          emails: [Plunk.Email.t()],
          workflow_executions: [Plunk.WorkflowExecution.t()],
          events: [Plunk.Event.t()],
          segment_memberships: [Plunk.SegmentMembership.t()],
          throttle_claims: [Plunk.ThrottleClaim.t()]
        }

  schema "contacts" do
    field(:email, :string)
    field(:data, :map)
    field(:subscribed, :boolean, default: true)
    field(:snoozed_until, Plunk.PrismaDateTime, source: :snoozedUntil)
    field(:project_id, :binary_id, source: :projectId)
    belongs_to(:project, Plunk.Project, foreign_key: :project_id, define_field: false)
    has_many(:emails, Plunk.Email, foreign_key: :contact_id)
    has_many(:workflow_executions, Plunk.WorkflowExecution, foreign_key: :contact_id)
    has_many(:events, Plunk.Event, foreign_key: :contact_id)
    has_many(:segment_memberships, Plunk.SegmentMembership, foreign_key: :contact_id)
    has_many(:throttle_claims, Plunk.ThrottleClaim, foreign_key: :contact_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end