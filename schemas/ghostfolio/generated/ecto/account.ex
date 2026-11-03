defmodule Ghostfolio.Account do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Ghostfolio.PrismaDateTime, autogenerate: {Ghostfolio.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          user_id: Ecto.UUID.t(),
          comment: String.t() | nil,
          currency: String.t() | nil,
          id: String.t(),
          name: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          platform: Ghostfolio.Platform.t() | nil,
          user: Ghostfolio.User.t() | nil
        }

  schema "Account" do
    field(:comment, :string)
    field(:currency, :string)
    field(:id, :string, primary_key: true, autogenerate: {Ecto.UUID, :generate, []})
    field(:name, :string)
    field(:platform_id, :binary_id, source: :platformId)
    field(:user_id, :binary_id, primary_key: true, source: :userId)
    belongs_to(:platform, Ghostfolio.Platform, foreign_key: :platform_id, define_field: false, type: :binary_id)
    belongs_to(:user, Ghostfolio.User, foreign_key: :user_id, define_field: false, type: :binary_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end