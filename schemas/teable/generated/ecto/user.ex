defmodule Teable.User do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Teable.PrismaDateTime, autogenerate: {Teable.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          password: String.t() | nil,
          salt: String.t() | nil,
          phone: String.t() | nil,
          email: String.t(),
          avatar: String.t() | nil,
          is_system: boolean() | nil,
          is_admin: boolean() | nil,
          is_trial_used: boolean() | nil,
          lang: String.t() | nil,
          notify_meta: String.t() | nil,
          last_sign_time: DateTime.t() | nil,
          deactivated_time: DateTime.t() | nil,
          created_time: DateTime.t(),
          deleted_time: DateTime.t() | nil,
          permanent_deleted_time: DateTime.t() | nil,
          ref_meta: String.t() | nil,
          updated_at: DateTime.t() | nil,
          accounts: [Teable.Account.t()],
          base_orders: [Teable.UserBaseOrder.t()]
        }

  schema "users" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:password, :string)
    field(:salt, :string)
    field(:phone, :string)
    field(:email, :string)
    field(:avatar, :string)
    field(:is_system, :boolean)
    field(:is_admin, :boolean)
    field(:is_trial_used, :boolean)
    field(:lang, :string)
    field(:notify_meta, :string)
    field(:last_sign_time, Teable.PrismaDateTime)
    field(:deactivated_time, Teable.PrismaDateTime)
    field(:created_time, Teable.PrismaDateTime, autogenerate: true)
    field(:deleted_time, Teable.PrismaDateTime)
    field(:permanent_deleted_time, Teable.PrismaDateTime)
    field(:ref_meta, :string)
    has_many(:accounts, Teable.Account, foreign_key: :user_id)
    has_many(:base_orders, Teable.UserBaseOrder, foreign_key: :user_id)
    timestamps(inserted_at: false, updated_at_source: :last_modified_time)
  end
end