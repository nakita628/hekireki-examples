defmodule Calcom.OutOfOfficeReason do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}

  @type t :: %__MODULE__{
          id: integer(),
          emoji: String.t(),
          reason: String.t(),
          enabled: boolean(),
          user: Calcom.User.t() | nil,
          entries: [Calcom.OutOfOfficeEntry.t()]
        }

  schema "OutOfOfficeReason" do
    field(:emoji, :string)
    field(:reason, :string)
    field(:enabled, :boolean, default: true)
    field(:user_id, :id, source: :userId)
    belongs_to(:user, Calcom.User, foreign_key: :user_id, define_field: false)
    has_many(:entries, Calcom.OutOfOfficeEntry, foreign_key: :reason_id)
  end
end