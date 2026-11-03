defmodule Hoppscotch.InfraConfig do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Hoppscotch.PrismaDateTime, autogenerate: {Hoppscotch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          value: String.t() | nil,
          is_encrypted: boolean(),
          last_synced_env_file_value: String.t() | nil,
          created_on: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "InfraConfig" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:value, :string)
    field(:is_encrypted, :boolean, default: false, source: :isEncrypted)
    field(:last_synced_env_file_value, :string, source: :lastSyncedEnvFileValue)
    field(:created_on, Hoppscotch.PrismaDateTime, autogenerate: true, source: :createdOn)
    timestamps(inserted_at: false, updated_at_source: :updatedOn)
  end
end