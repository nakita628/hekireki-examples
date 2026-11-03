defmodule Trigger.RevokedApiKey do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          api_key: String.t(),
          expires_at: DateTime.t(),
          inserted_at: DateTime.t(),
          runtime_environment: Trigger.RuntimeEnvironment.t() | nil
        }

  schema "RevokedApiKey" do
    field(:id, :string, primary_key: true)
    field(:api_key, :string, source: :apiKey)
    field(:expires_at, Trigger.PrismaDateTime, source: :expiresAt)
    field(:runtime_environment_id, :string, source: :runtimeEnvironmentId)
    belongs_to(:runtime_environment, Trigger.RuntimeEnvironment, foreign_key: :runtime_environment_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end