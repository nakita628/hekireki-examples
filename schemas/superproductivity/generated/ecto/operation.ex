defmodule SuperProductivity.Operation do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          client_id: String.t(),
          server_seq: integer(),
          action_type: String.t(),
          op_type: String.t(),
          entity_type: String.t(),
          entity_id: String.t() | nil,
          entity_ids: [String.t()],
          payload: map(),
          payload_bytes: integer(),
          vector_clock: map(),
          schema_version: integer(),
          client_timestamp: integer(),
          received_at: integer(),
          is_payload_encrypted: boolean(),
          sync_import_reason: String.t() | nil,
          repair_base_server_seq: integer() | nil,
          user: SuperProductivity.User.t() | nil
        }

  schema "operations" do
    field(:id, :string, primary_key: true)
    field(:client_id, :string)
    field(:server_seq, :integer)
    field(:action_type, :string)
    field(:op_type, :string)
    field(:entity_type, :string)
    field(:entity_id, :string)
    field(:entity_ids, {:array, :string})
    field(:payload, :map)
    field(:payload_bytes, :integer, default: 0)
    field(:vector_clock, :map)
    field(:schema_version, :integer)
    field(:client_timestamp, :integer)
    field(:received_at, :integer)
    field(:is_payload_encrypted, :boolean, default: false)
    field(:sync_import_reason, :string)
    field(:repair_base_server_seq, :integer)
    belongs_to(:user, SuperProductivity.User, foreign_key: :user_id)
  end
end