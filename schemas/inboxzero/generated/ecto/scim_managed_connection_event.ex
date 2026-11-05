defmodule InboxZero.ScimManagedConnectionEvent do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          event_key: String.t(),
          sequence: integer(),
          type: String.t(),
          actor_id: String.t(),
          credential_id: String.t() | nil,
          created_at: DateTime.t(),
          scimmanagedconnection: InboxZero.ScimManagedConnection.t() | nil
        }

  schema "scimManagedConnectionEvent" do
    field(:id, :string, primary_key: true)
    field(:event_key, :string, source: :eventKey)
    field(:sequence, :integer)
    field(:type, :string)
    field(:actor_id, :string, source: :actorId)
    field(:credential_id, :string, source: :credentialId)
    field(:created_at, InboxZero.PrismaDateTime, source: :createdAt)
    field(:connection_record_id, :string, source: :connectionRecordId)
    belongs_to(:scimmanagedconnection, InboxZero.ScimManagedConnection, foreign_key: :connection_record_id, define_field: false, type: :string)
  end
end