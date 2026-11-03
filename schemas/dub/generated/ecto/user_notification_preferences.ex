defmodule Dub.UserNotificationPreferences do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          dub_links: boolean(),
          user: Dub.User.t() | nil
        }

  schema "UserNotificationPreferences" do
    field(:id, :string, primary_key: true)
    field(:dub_links, :boolean, default: true, source: :dubLinks)
    field(:user_id, :string, source: :userId)
    belongs_to(:user, Dub.User, foreign_key: :user_id, define_field: false, type: :string)
  end
end