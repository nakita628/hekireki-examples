defmodule LiteLLM.LiteLLMJWTKeyMapping do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          jwt_issuer: String.t(),
          jwt_claim_name: String.t(),
          jwt_claim_value: String.t(),
          description: String.t() | nil,
          is_active: boolean(),
          created_by: String.t() | nil,
          updated_by: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          litellm_verification_token: LiteLLM.LiteLLMVerificationToken.t() | nil
        }

  schema "LiteLLM_JWTKeyMapping" do
    field(:jwt_issuer, :string, default: "")
    field(:jwt_claim_name, :string)
    field(:jwt_claim_value, :string)
    field(:description, :string)
    field(:is_active, :boolean, default: true)
    field(:created_by, :string)
    field(:updated_by, :string)
    belongs_to(:litellm_verification_token, LiteLLM.LiteLLMVerificationToken, foreign_key: :token, type: :string, references: :token)
    timestamps(inserted_at_source: :created_at)
  end
end