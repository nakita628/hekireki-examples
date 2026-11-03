defmodule Hoppscotch.PersonalAccessToken do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Hoppscotch.PrismaDateTime, autogenerate: {Hoppscotch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          label: String.t(),
          token: String.t(),
          expires_on: DateTime.t() | nil,
          created_on: DateTime.t(),
          updated_at: DateTime.t(),
          user: Hoppscotch.User.t() | nil
        }

  schema "PersonalAccessToken" do
    field(:id, :string, primary_key: true)
    field(:label, :string)
    field(:token, :string, autogenerate: {Ecto.UUID, :generate, []})
    field(:expires_on, Hoppscotch.PrismaDateTime, source: :expiresOn)
    field(:created_on, Hoppscotch.PrismaDateTime, autogenerate: true, source: :createdOn)
    field(:user_uid, :string, source: :userUid)
    belongs_to(:user, Hoppscotch.User, foreign_key: :user_uid, define_field: false, type: :string, references: :uid)
    timestamps(inserted_at: false, updated_at_source: :updatedOn)
  end
end