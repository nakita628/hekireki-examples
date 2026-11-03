defmodule Teable.UserBaseOrder do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Teable.PrismaDateTime, autogenerate: {Teable.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          order: float(),
          created_time: DateTime.t(),
          updated_at: DateTime.t() | nil,
          user: Teable.User.t() | nil,
          base: Teable.Base.t() | nil
        }

  schema "user_base_order" do
    field(:id, :string, primary_key: true)
    field(:order, :float)
    field(:created_time, Teable.PrismaDateTime, autogenerate: true)
    belongs_to(:user, Teable.User, foreign_key: :user_id, type: :string)
    belongs_to(:base, Teable.Base, foreign_key: :base_id, type: :string)
    timestamps(inserted_at: false, updated_at_source: :last_modified_time)
  end
end