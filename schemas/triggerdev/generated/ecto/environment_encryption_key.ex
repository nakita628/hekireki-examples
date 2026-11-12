defmodule Trigger.EnvironmentEncryptionKey do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          wrapped_key: map(),
          wrapping_provider: atom(),
          wrapping_key_reference: String.t(),
          revision: integer(),
          inserted_at: DateTime.t(),
          environment: Trigger.RuntimeEnvironment.t() | nil
        }

  schema "EnvironmentEncryptionKey" do
    field(:id, :string, primary_key: true)
    field(:wrapped_key, :map, source: :wrappedKey)
    field(:wrapping_provider, Ecto.Enum, values: [:PLATFORM_KEYRING, :AWS_KMS], source: :wrappingProvider)
    field(:wrapping_key_reference, :string, source: :wrappingKeyReference)
    field(:revision, :integer, default: 0)
    field(:environment_id, :string, source: :environmentId)
    belongs_to(:environment, Trigger.RuntimeEnvironment, foreign_key: :environment_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end