defmodule Typebot.UserCredentials do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Typebot.PrismaDateTime, autogenerate: {Typebot.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          data: String.t(),
          name: String.t(),
          type: String.t(),
          iv: String.t(),
          inserted_at: DateTime.t(),
          user: Typebot.User.t() | nil
        }

  schema "UserCredentials" do
    field(:id, :string, primary_key: true)
    field(:data, :string)
    field(:name, :string)
    field(:type, :string)
    field(:iv, :string)
    field(:user_id, :string, source: :userId)
    belongs_to(:user, Typebot.User, foreign_key: :user_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end