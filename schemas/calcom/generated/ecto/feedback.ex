defmodule Calcom.Feedback do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}

  @type t :: %__MODULE__{
          id: integer(),
          date: DateTime.t(),
          rating: String.t(),
          comment: String.t() | nil,
          user: Calcom.User.t() | nil
        }

  schema "Feedback" do
    field(:date, Calcom.PrismaDateTime, autogenerate: true)
    field(:rating, :string)
    field(:comment, :string)
    field(:user_id, :id, source: :userId)
    belongs_to(:user, Calcom.User, foreign_key: :user_id, define_field: false)
  end
end