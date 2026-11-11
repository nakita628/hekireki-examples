defmodule LiteLLM.LiteLLMSSOIdentityAssertion do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LiteLLM.PrismaDateTime, autogenerate: {LiteLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          user_id: String.t(),
          assertion_b64: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "LiteLLM_SSOIdentityAssertion" do
    field(:user_id, :string, primary_key: true)
    field(:assertion_b64, :string)
    timestamps(inserted_at_source: :created_at)
  end
end