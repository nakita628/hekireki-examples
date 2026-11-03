defmodule Calcom.NotificationsSubscriptions do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}

  @type t :: %__MODULE__{
          id: integer(),
          subscription: String.t(),
          user: Calcom.User.t() | nil
        }

  schema "NotificationsSubscriptions" do
    field(:subscription, :string)
    field(:user_id, :id, source: :userId)
    belongs_to(:user, Calcom.User, foreign_key: :user_id, define_field: false)
  end
end