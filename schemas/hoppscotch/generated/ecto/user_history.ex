defmodule Hoppscotch.UserHistory do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          req_type: atom(),
          request: map(),
          response_metadata: map(),
          is_starred: boolean(),
          executed_on: DateTime.t(),
          user: Hoppscotch.User.t() | nil
        }

  schema "UserHistory" do
    field(:id, :string, primary_key: true)
    field(:req_type, Ecto.Enum, values: [:REST, :GQL], source: :reqType)
    field(:request, :map)
    field(:response_metadata, :map, source: :responseMetadata)
    field(:is_starred, :boolean, source: :isStarred)
    field(:executed_on, Hoppscotch.PrismaDateTime, autogenerate: true, source: :executedOn)
    field(:user_uid, :string, source: :userUid)
    belongs_to(:user, Hoppscotch.User, foreign_key: :user_uid, define_field: false, type: :string, references: :uid)
  end
end