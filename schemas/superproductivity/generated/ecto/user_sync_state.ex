defmodule SuperProductivity.UserSyncState do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          last_seq: integer(),
          last_snapshot_seq: integer() | nil,
          snapshot_data: binary() | nil,
          snapshot_at: integer() | nil,
          snapshot_schema_version: integer() | nil,
          latest_full_state_seq: integer() | nil,
          latest_full_state_vector_clock: map() | nil,
          latest_state_replacement_seq: integer() | nil,
          user: SuperProductivity.User.t() | nil
        }

  schema "user_sync_state" do
    field(:last_seq, :integer, default: 0)
    field(:last_snapshot_seq, :integer)
    field(:snapshot_data, :binary)
    field(:snapshot_at, :integer)
    field(:snapshot_schema_version, :integer, default: 1)
    field(:latest_full_state_seq, :integer)
    field(:latest_full_state_vector_clock, :map)
    field(:latest_state_replacement_seq, :integer)
    belongs_to(:user, SuperProductivity.User, foreign_key: :user_id)
  end
end