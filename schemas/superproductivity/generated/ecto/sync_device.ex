defmodule SuperProductivity.SyncDevice do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          user_id: term(),
          client_id: String.t(),
          device_name: String.t() | nil,
          user_agent: String.t() | nil,
          app_version: String.t() | nil,
          last_seen_at: integer(),
          last_acked_seq: integer(),
          created_at: integer(),
          user: SuperProductivity.User.t() | nil
        }

  schema "sync_devices" do
    field(:client_id, :string, primary_key: true)
    field(:device_name, :string)
    field(:user_agent, :string)
    field(:app_version, :string)
    field(:last_seen_at, :integer)
    field(:last_acked_seq, :integer, default: 0)
    field(:created_at, :integer)
    field(:user_id, :id, primary_key: true)
    belongs_to(:user, SuperProductivity.User, foreign_key: :user_id, define_field: false)
  end
end