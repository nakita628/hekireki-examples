defmodule InboxZero.ScimManagedConnection do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          creation_request_id: String.t(),
          connection_id: String.t(),
          provisioning_domain_id: String.t(),
          status: String.t(),
          revision: integer(),
          created_at: DateTime.t(),
          created_by: String.t(),
          decommission_started_at: DateTime.t() | nil,
          decommission_started_by: String.t() | nil,
          decommissioned_at: DateTime.t() | nil,
          decommissioned_by: String.t() | nil,
          scimmanagedcredentials: [InboxZero.ScimManagedCredential.t()],
          scimmanagedconnectionevents: [InboxZero.ScimManagedConnectionEvent.t()]
        }

  schema "scimManagedConnection" do
    field(:id, :string, primary_key: true)
    field(:creation_request_id, :string, source: :creationRequestId)
    field(:connection_id, :string, source: :connectionId)
    field(:provisioning_domain_id, :string, source: :provisioningDomainId)
    field(:status, :string)
    field(:revision, :integer)
    field(:created_at, InboxZero.PrismaDateTime, source: :createdAt)
    field(:created_by, :string, source: :createdBy)
    field(:decommission_started_at, InboxZero.PrismaDateTime, source: :decommissionStartedAt)
    field(:decommission_started_by, :string, source: :decommissionStartedBy)
    field(:decommissioned_at, InboxZero.PrismaDateTime, source: :decommissionedAt)
    field(:decommissioned_by, :string, source: :decommissionedBy)
    has_many(:scimmanagedcredentials, InboxZero.ScimManagedCredential, foreign_key: :connection_record_id, references: :id)
    has_many(:scimmanagedconnectionevents, InboxZero.ScimManagedConnectionEvent, foreign_key: :connection_record_id, references: :id)
  end
end