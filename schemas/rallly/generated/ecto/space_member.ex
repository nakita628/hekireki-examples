defmodule Rallly.SpaceMember do
  use Ecto.Schema

  @primary_key {:id, :binary_id, autogenerate: true}
  @foreign_key_type :binary_id
  @timestamps_opts [type: Rallly.PrismaDateTime, autogenerate: {Rallly.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: Ecto.UUID.t(),
          role: atom(),
          last_selected_at: DateTime.t(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          space: Rallly.Space.t() | nil,
          user: Rallly.User.t() | nil
        }

  schema "space_members" do
    field(:role, Ecto.Enum, values: [:ADMIN, :MEMBER], default: :MEMBER)
    field(:last_selected_at, Rallly.PrismaDateTime, autogenerate: true)
    belongs_to(:space, Rallly.Space, foreign_key: :space_id)
    belongs_to(:user, Rallly.User, foreign_key: :user_id, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end