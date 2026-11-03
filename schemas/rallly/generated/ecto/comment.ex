defmodule Rallly.Comment do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Rallly.PrismaDateTime, autogenerate: {Rallly.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          content: String.t(),
          author_name: String.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t() | nil,
          poll: Rallly.Poll.t() | nil,
          user: Rallly.User.t() | nil
        }

  schema "comments" do
    field(:id, :string, primary_key: true)
    field(:content, :string)
    field(:author_name, :string)
    belongs_to(:poll, Rallly.Poll, foreign_key: :poll_id, type: :string)
    belongs_to(:user, Rallly.User, foreign_key: :user_id, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end