defmodule InboxZero.ScimSubject do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          profile_source_id: String.t() | nil,
          revision: integer(),
          created_at: DateTime.t(),
          updated_at: DateTime.t(),
          user: InboxZero.User.t() | nil
        }

  schema "scimSubject" do
    field(:id, :string, primary_key: true)
    field(:profile_source_id, :string, source: :profileSourceId)
    field(:revision, :integer)
    field(:created_at, InboxZero.PrismaDateTime, source: :createdAt)
    field(:updated_at, InboxZero.PrismaDateTime, source: :updatedAt)
    field(:user_id, :string, source: :userId)
    belongs_to(:user, InboxZero.User, foreign_key: :user_id, define_field: false, type: :string)
  end
end