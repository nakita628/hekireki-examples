defmodule Trigger.ApiKey do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          key_hash: String.t(),
          last_four: String.t(),
          description: String.t() | nil,
          preset_id: String.t() | nil,
          scopes: [String.t()],
          allowed_ip_ranges: [String.t()],
          last_used_at: DateTime.t() | nil,
          last_used_ip: String.t() | nil,
          revoked_at: DateTime.t() | nil,
          expires_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          runtime_environment: Trigger.RuntimeEnvironment.t() | nil,
          created_by: Trigger.User.t() | nil
        }

  schema "api_keys" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:key_hash, :string)
    field(:last_four, :string)
    field(:description, :string)
    field(:preset_id, :string)
    field(:scopes, {:array, :string})
    field(:allowed_ip_ranges, {:array, :string})
    field(:last_used_at, Trigger.PrismaDateTime)
    field(:last_used_ip, :string)
    field(:revoked_at, Trigger.PrismaDateTime)
    field(:expires_at, Trigger.PrismaDateTime)
    belongs_to(:runtime_environment, Trigger.RuntimeEnvironment, foreign_key: :runtime_environment_id, type: :string)
    belongs_to(:created_by, Trigger.User, foreign_key: :created_by_user_id, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end