defmodule InboxZero.ScimIdentityLink do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          connection_id: String.t(),
          external_id: String.t(),
          user: InboxZero.User.t() | nil
        }

  schema "ScimIdentityLink" do
    field(:id, :string, primary_key: true)
    field(:connection_id, :string, source: :connectionId)
    field(:external_id, :string, source: :externalId)
    field(:user_id, :string, source: :userId)
    belongs_to(:user, InboxZero.User, foreign_key: :user_id, define_field: false, type: :string)
  end
end