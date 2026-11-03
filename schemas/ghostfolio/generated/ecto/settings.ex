defmodule Ghostfolio.Settings do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Ghostfolio.PrismaDateTime, autogenerate: {Ghostfolio.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          settings: map() | nil,
          updated_at: DateTime.t(),
          user: Ghostfolio.User.t() | nil
        }

  schema "Settings" do
    field(:settings, :map)
    field(:user_id, :binary_id, source: :userId)
    belongs_to(:user, Ghostfolio.User, foreign_key: :user_id, define_field: false, type: :binary_id)
    timestamps(inserted_at: false, updated_at_source: :updatedAt)
  end
end