defmodule Trigger.EnvironmentEncryptionPolicy do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          revision: integer(),
          wrapping_provider: atom() | nil,
          wrapping_key_reference: String.t() | nil,
          active_key_id: String.t() | nil,
          environment: Trigger.RuntimeEnvironment.t() | nil
        }

  schema "EnvironmentEncryptionPolicy" do
    field(:revision, :integer, default: 0)
    field(:wrapping_provider, Ecto.Enum, values: [:PLATFORM_KEYRING, :AWS_KMS], source: :wrappingProvider)
    field(:wrapping_key_reference, :string, source: :wrappingKeyReference)
    field(:active_key_id, :string, source: :activeKeyId)
    field(:environment_id, :string, source: :environmentId)
    belongs_to(:environment, Trigger.RuntimeEnvironment, foreign_key: :environment_id, define_field: false, type: :string)
  end
end