defmodule LiteLLM.LiteLLMUserNotifications do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          request_id: String.t(),
          user_id: String.t(),
          models: [String.t()],
          justification: String.t(),
          status: String.t()
        }

  schema "LiteLLM_UserNotifications" do
    field(:request_id, :string, primary_key: true)
    field(:user_id, :string)
    field(:models, {:array, :string})
    field(:justification, :string)
    field(:status, :string)
  end
end