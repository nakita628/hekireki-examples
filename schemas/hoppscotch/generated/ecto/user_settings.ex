defmodule Hoppscotch.UserSettings do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Hoppscotch.PrismaDateTime, autogenerate: {Hoppscotch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          properties: map(),
          updated_at: DateTime.t(),
          user: Hoppscotch.User.t() | nil
        }

  schema "UserSettings" do
    field(:id, :string, primary_key: true)
    field(:properties, :map)
    field(:user_uid, :string, source: :userUid)
    belongs_to(:user, Hoppscotch.User, foreign_key: :user_uid, define_field: false, type: :string, references: :uid)
    timestamps(inserted_at: false, updated_at_source: :updatedOn)
  end
end